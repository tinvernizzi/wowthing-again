<script lang="ts">
    import { achievementStore, userAchievementStore } from '@/stores'

    import AchievementsAchievement from './AchievementsAchievement.svelte'
    import ProgressBar from '@/components/common/ProgressBar.svelte'
</script>

<style lang="scss">
    .summary {
        width: 100%;

        @media screen and (min-width: 1600px) {
            align-items: flex-start;
            column-gap: 1rem;
            display: grid;
            grid-template-columns: 1fr 1fr;
        }
    }
    .summary-points {
        border: 1px solid $border-color;
        margin-bottom: 1rem;
        padding: 1rem;
        width: 100%;
    }
    .summary-categories {
        --progress-margin-top: 1rem;

        width: 100%;

        @media screen and (min-width: 1100px) {
            column-gap: 1rem;
            display: grid;
            grid-template-columns: 1fr 1fr;
        }
    }
</style>

<div class="summary">
    <div class="summary-points thing-container">
        <ProgressBar
            title="Overall"
            have={$userAchievementStore.data.achievementCategories[0].have}
            total={$userAchievementStore.data.achievementCategories[0].total}
            --bar-height="2.5rem"
        />

        <div class="summary-categories">
            {#each $achievementStore.data.categories as category}
                <ProgressBar
                    title={category.name}
                    have={$userAchievementStore.data.achievementCategories[category.id].have}
                    total={$userAchievementStore.data.achievementCategories[category.id].total}
                />
            {/each}
        </div>
    </div>

    <div class="summary-recent">
        {#each $userAchievementStore.data.achievementRecent as recent}
            <AchievementsAchievement achievementId={recent} />
        {/each}
    </div>
</div>
