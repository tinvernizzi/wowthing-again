import { UserCount, WritableFancyStore } from '@/types'
import type { UserTransmogData } from '@/types/data'
import type { JournalData } from '@/types/data/journal'


export class JournalDataStore extends WritableFancyStore<JournalData> {
    get dataUrl(): string {
        return document
            .getElementById('app')
            ?.getAttribute('data-journal')
    }

    setup(
        journalData: JournalData,
        userTransmogData: UserTransmogData
    ): void {
        console.time('JournalDataStore.initialize')

        const stats: Record<string, UserCount> = {}

        const overallStats = stats['OVERALL'] = new UserCount()

        for (const tier of journalData.tiers) {
            const tierStats = stats[tier.slug] = new UserCount()

            for (const instance of tier.instances) {
                const instanceKey = `${tier.slug}--${instance.slug}`
                const instanceStats = stats[instanceKey] = new UserCount()

                for (const encounter of instance.encounters) {
                    const encounterKey = `${instanceKey}--${encounter.name}`
                    const encounterStats = stats[encounterKey] = new UserCount()

                    for (const item of encounter.items) {
                        for (const appearance of item.appearances) {
                            overallStats.total++
                            tierStats.total++
                            instanceStats.total++
                            encounterStats.total++
                            
                            if (userTransmogData.userHas[appearance.appearanceId]) {
                                overallStats.have++
                                tierStats.have++
                                instanceStats.have++
                                encounterStats.have++
                            }
                        }
                    }
                }
            }
        }

        console.log(stats)

        this.update((state) => {
            state.data.stats = stats
            return state
        })

        console.timeEnd('JournalDataStore.initialize')
    }
}

export const journalStore = new JournalDataStore()