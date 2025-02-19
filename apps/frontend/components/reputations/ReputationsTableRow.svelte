<script lang="ts">
    import { staticStore } from '@/stores/static'
    import findReputationTier from '@/utils/find-reputation-tier'
    import { tippyComponent } from '@/utils/tippy'
    import type { Character, CharacterReputationParagon, CharacterReputationReputation, ReputationTier } from '@/types'
    import type { StaticDataReputation, StaticDataReputationSet, StaticDataReputationTier } from '@/types/data/static'

    import TooltipReputation from '@/components/tooltips/reputation/TooltipReputation.svelte'

    export let character: Character
    export let characterRep: CharacterReputationReputation
    export let reputation: StaticDataReputationSet

    let dataRep: StaticDataReputation
    let paragon: CharacterReputationParagon
    let repTier: ReputationTier

    $: {
        if (reputation !== undefined && characterRep.value !== -1) {
            dataRep = $staticStore.data.reputations[characterRep.reputationId]
            const tiers: StaticDataReputationTier = $staticStore.data.reputationTiers[dataRep.tierId] || $staticStore.data.reputationTiers[0]
            repTier = findReputationTier(tiers, characterRep.value)

            if (reputation.paragon && repTier.maxValue === 0) {
                paragon = character.paragons?.[characterRep.reputationId]
                if (paragon) {
                    if (paragon.rewardAvailable) {
                        repTier.percent = 'BOX'
                    }
                    else {
                        repTier.percent = ((paragon.current / paragon.max) * 100).toFixed(1)
                    }
                }
            }
            else {
                paragon = undefined
            }
        }
    }
</script>

<style lang="scss">
    td {
        border-left: 1px solid $border-color;
        text-align: center;
    }
</style>

{#if characterRep.value !== -1}
    <td
        class="reputation{repTier.tier}"
        use:tippyComponent={{
            component: TooltipReputation,
            props: {
                characterRep: characterRep.value,
                character,
                dataRep,
                paragon,
                reputation,
            }
        }}
    >
        {#if paragon}
            {#if paragon.rewardAvailable}
                <span class="status-fail">BOX!</span>
            {:else}
                {repTier.percent}%
            {/if}
        {:else}
            {repTier.percent}%
        {/if}
    </td>
{:else}
    <td>&nbsp;</td>
{/if}
