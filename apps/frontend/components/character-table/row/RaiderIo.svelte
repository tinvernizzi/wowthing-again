<script lang="ts">
    import { seasonMap } from '@/data/dungeon'
    import { staticStore } from '@/stores/static'
    import { Region } from '@/types/enums'
    import getRaiderIoColor from'@/utils/get-raider-io-color'
    import { tippyComponent } from '@/utils/tippy'
    import type { Character, CharacterRaiderIoSeason, MythicPlusSeason } from '@/types'
    import type { StaticDataRaiderIoScoreTiers } from '@/types/data/static'

    import Tooltip from '@/components/tooltips/mythic-plus-score/TooltipMythicPlusScore.svelte'

    export let character: Character
    export let season: MythicPlusSeason = null
    export let seasonId = 0

    let overallScore: number
    let region: string
    let scores: CharacterRaiderIoSeason
    let tiers: StaticDataRaiderIoScoreTiers
    $: {
        if (seasonId > 0) {
            season = seasonMap[seasonId]
        }
        if (season) {
            scores = character.raiderIo?.[season.id]
            tiers = $staticStore.data.raiderIoScoreTiers[season.id]

            overallScore = character.mythicPlusSeasonScores[season.id] || scores?.['all'] || 0
        }
        region = Region[character.realm.region].toLowerCase()
    }
</script>

<style lang="scss">
    .score {
        @include cell-width($width-raider-io);

        border-left: 1px solid $border-color;
        text-align: right;
    }
</style>

{#if overallScore > 0}
    <td
        class="score"
        style:--link-color={getRaiderIoColor(tiers, overallScore)}
        use:tippyComponent={{
            component: Tooltip,
            props: {
                seasonId: season.id,
                character,
                scores,
                tiers
            },
        }}
    >
        <a
            href="https://raider.io/characters/{region}/{character.realm.slug}/{character.name}"
            rel="noopener noreferrer"
            target="_blank"
        >
            {overallScore.toFixed(1)}
        </a>
    </td>
{:else}
    <td class="score">&nbsp;</td>
{/if}
