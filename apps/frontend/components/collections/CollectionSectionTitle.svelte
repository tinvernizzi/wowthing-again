<script lang="ts">
    import getPercentClass from '@/utils/get-percent-class'
    import type { UserCount } from '@/types'

    import Count from '@/components/collections/CollectionCount.svelte'
    import ParsedText from '../common/ParsedText.svelte'

    export let count: UserCount
    export let title: string

    let percent: number
    $: {
        percent = Math.floor((count?.have ?? 0) / (count?.total ?? 1) * 100)
    }
</script>

<style lang="scss">
    div {
        align-items: center;
        background: $collection-background;
        border-bottom: 1px solid $border-color;
        color: #ddd;
        display: flex;
        padding: 0.25rem 0.5rem;
        width: 100%;

        &:first-child {
            border-top-left-radius: $border-radius;
            border-top-right-radius: $border-radius;
        }

        &:not(:first-child) {
            border-top: 1px solid $border-color;
        }
    }
    h3 {
        flex: 0 0 auto;
        margin: 0;
    }
</style>

<div>
    <h3 class="{getPercentClass(percent)}">
        <ParsedText text={title} />
    </h3>
    <Count counts={count} />
    <slot />
</div>
