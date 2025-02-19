<script lang="ts">
    import mdiCheckboxOutline from '@iconify/icons-mdi/check-circle-outline'
    import xor from 'lodash/xor'

    import { difficultyMap, journalDifficultyOrder } from '@/data/difficulty'
    import { userStore, userTransmogStore } from '@/stores'
    import { journalState } from '@/stores/local-storage'
    import { data as settingsData } from '@/stores/settings'
    import { PlayableClass, PlayableClassMask, RewardType } from '@/types/enums'
    import { getItemUrl } from '@/utils/get-item-url'
    import tippy from '@/utils/tippy'
    import type { JournalDataEncounterItem, JournalDataEncounterItemAppearance } from '@/types/data/journal'

    import ClassIcon from '@/components/images/ClassIcon.svelte'
    import IconifyIcon from '@/components/images/IconifyIcon.svelte'
    import WowthingImage from '@/components/images/sources/WowthingImage.svelte'

    export let bonusIds: Record<number, number> = undefined
    export let item: JournalDataEncounterItem

    let appearances: [JournalDataEncounterItemAppearance, boolean][]
    let classId: number
    $: {
        if (item.type === RewardType.Mount) {
            appearances = item.appearances.map((appearance) => [
                appearance,
                $userStore.data.hasMount[item.classId],
            ])
        }
        else if (item.type === RewardType.Pet) {
            appearances = item.appearances.map((appearance) => [
                appearance,
                $userStore.data.hasPet[item.classId],
            ])
        }
        else if (item.type === RewardType.Toy) {
            appearances = item.appearances.map((appearance) => [
                appearance,
                $userStore.data.hasToy[item.id],
            ])
        }
        else {
            appearances = item.appearances.map((appearance) => [
                appearance,
                $settingsData.transmog.completionistMode ?
                    $userTransmogStore.data.sourceHas[`${item.id}_${appearance.modifierId}`] :
                    $userTransmogStore.data.userHas[appearance.appearanceId],
            ])
        }

        if (item.classMask in PlayableClassMask) {
            classId = PlayableClass[PlayableClassMask[item.classMask] as keyof typeof PlayableClass]
        }
        else {
            classId = 0
        }

        //console.log(item, appearances)
    }

    const getQuality = function(appearance: JournalDataEncounterItemAppearance): number {
        // Mythic Keystone/Mythic difficulties should probably set the quality to epic?
        for (const difficulty of appearance.difficulties) {
            if (difficulty === 23) {
                return 4
            }
        }
        return item.quality
    }

    const getBonusIds = function(appearance: JournalDataEncounterItemAppearance): number[] {
        if (bonusIds === undefined || appearance.difficulties?.length > 1) {
            return []
        }

        const bonusId = bonusIds[appearance.difficulties[0]]
        return bonusId ? [bonusId] : []
    }

    const getDifficulties = function(appearance: JournalDataEncounterItemAppearance): [string[], string[]] {
        if (!appearance.difficulties) {
            return [[], []]
        }

        // 10 Normal + 10 Heroic = 10
        if (xor(appearance.difficulties, [3, 5]).length === 0) {
            return [['10NH'], ['10 Normal / 10 Heroic']]
        }
        // 25 Normal + 25 Heroic = 25
        if (xor(appearance.difficulties, [4, 6]).length === 0) {
            return [['25NH'], ['25 Normal / 25 Heroic']]
        }
        // 10 Normal + 25 Normal + Normal? = Normal
        if (xor(appearance.difficulties, [3, 4]).length === 0 ||
            xor(appearance.difficulties, [3, 4, 14]).length === 0) {
            return [['N'], ['Normal']]
        }
        // 10 Heroic + 25 Heroic + Heroic? = Heroic
        if (xor(appearance.difficulties, [5, 6]).length === 0 ||
            xor(appearance.difficulties, [5, 6, 15]).length === 0) {
            return [['H'], ['Heroic']]
        }
        // 10 Normal + 25 Normal + 10 Heroic + 25 Heroic = Normal/Heroic (ZA/ZG)
        if (xor(appearance.difficulties, [3, 4, 5, 6]).length === 0) {
            return [['N', 'H'], ['Normal', 'Heroic']]
        }
        // 10 Normal + 25 Normal + LFR = LFR/Normal/Heroic
        if (xor(appearance.difficulties, [3, 4, 7]).length === 0) {
            return [['L', 'N', 'H'], ['LFR', 'Normal', 'Heroic']]
        }
        // 10 Normal + 25 Normal + 10 Heroic + 25 Heroic + LFR = LFR/Normal/Heroic
        if (xor(appearance.difficulties, [3, 4, 5, 6, 7]).length === 0) {
            return [['L', 'N', 'H'], ['LFR', 'Normal', 'Heroic']]
        }

        const ret: [string[], string[]] = [[], []]
        for (const difficulty of journalDifficultyOrder) {
            if (appearance.difficulties.indexOf(difficulty) >= 0) {
                ret[0].push(difficultyMap[difficulty].shortName)
                ret[1].push(difficultyMap[difficulty].name)
            }
        }
        return ret
    }
</script>

<style lang="scss">
    .journal-item {
        min-height: 52px;
        width: 52px;

        :global(img) {
            border-bottom-left-radius: 0;
            border-bottom-right-radius: 0;

        }
    }
    .player-class {
        --image-margin-top: -4px;
        --shadow-color: rgba(0, 0, 0, 0.8);

        border: none;
        height: 24px;
        left: -1px;
        width: 24px;
        position: absolute;
        top: -1px;
    }
    .collected-appearances {
        border-bottom-left-radius: 0;
        border-top-right-radius: 0;
        color: $colour-success;
        line-height: 1;
        padding: 0.1rem 0.2rem;
        pointer-events: none;
        position: absolute;
        top: 30px;
        right: 1px;
    }
    .difficulties {
        background-color: $highlight-background;
        border: 1px solid;
        border-radius: $border-radius-small;
        border-top-left-radius: 0;
        border-top-right-radius: 0;
        line-height: 1;
        margin-top: -1px;
        padding: 0 2px 1px 2px;
        text-align: center;
        white-space: nowrap;
    }
</style>

{#each appearances as [appearance, userHas]}
    {#if
        ($journalState.showCollected && userHas) ||
        ($journalState.showUncollected && !userHas)
    }
        {@const [diffShort, diffLong] = getDifficulties(appearance)}
        <div
            class="journal-item quality{getQuality(appearance)}"
            class:missing={
                (!$journalState.highlightMissing && !userHas) ||
                ($journalState.highlightMissing && userHas)
            }
        >
            <a href="{getItemUrl({
                itemId: item.id,
                bonusIds: getBonusIds(appearance),
            })}">
                <WowthingImage
                    name="item/{item.id}{appearance.modifierId > 0 ? `_${appearance.modifierId}` : ''}"
                    size={48}
                    border={2}
                />
            </a>

            {#if classId > 0}
                <div class="player-class class-{classId} drop-shadow">
                    <ClassIcon
                        border={2}
                        size={20}
                        {classId}
                    />
                </div>
            {/if}
            
            {#if item.extraAppearances > 0}
                <div class="collected-appearances background-box drop-shadow">
                    +{item.extraAppearances}
                </div>
            {/if}

            {#if userHas}
                <div class="collected-icon drop-shadow">
                    <IconifyIcon icon={mdiCheckboxOutline} />
                </div>
            {/if}

            <div class="difficulties" use:tippy={diffLong.join(' / ')}>
                {#each diffShort as difficulty}
                    <span>{difficulty}</span>
                {/each}
            </div>
        </div>
    {/if}
{/each}
