<script lang="ts">
    import { onMount } from 'svelte'

    import { achievementStore, userAchievementStore } from '@/stores'
    import { data as settings } from '@/stores/settings'
    
    import AchievementsCategory from './AchievementsCategory.svelte'
    import AchievementsSidebar from './AchievementsSidebar.svelte'
    import AchievementsScoreSummary from './AchievementsScoreSummary.svelte'

    export let params: {
        slug1: string
        slug2: string
    }

    // Fetch achievement data once when this component is mounted
    onMount(async () => await Promise.all([
        achievementStore.fetch({ language: $settings.general.language }),
        //userAchievementStore.fetch(),
    ]))

    let error: boolean
    let loaded: boolean
    let ready: boolean
    $: {
        error = $achievementStore.error || $userAchievementStore.error
        loaded = $achievementStore.loaded && $userAchievementStore.loaded
        ready = false
        if (!error && loaded) {
            if (!$userAchievementStore.data.achievementCategories) {
                userAchievementStore.setup($achievementStore.data)
            }
            ready = true
        }
    }
</script>

<style lang="scss">
    div {
        align-items: flex-start;
        display: flex;
        width: 100%;
    }
</style>

<div class="wrapper">
    {#if error}
        <p>KABOOM! Something has gone horribly wrong, try reloading the page?</p>
    {:else if !ready}
        <p>L O A D I N G</p>
    {:else}
        <AchievementsSidebar />
        {#if params.slug1 === 'summary'}
            <AchievementsScoreSummary />
        {:else}
            <AchievementsCategory slug1={params.slug1} slug2={params.slug2} />
        {/if}
    {/if}
</div>
