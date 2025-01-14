import { Faction } from '@/types/enums'
import { getCurrencyCostsString } from '@/utils/get-currency-costs'
import type { ManualData } from './store'
import type { UserCount } from '@/types'
import type { StaticData } from '@/types/data/static/store'
import type { RewardType, ItemQuality,  } from '@/types/enums'


export class ManualDataVendorCategory {
    public groups: ManualDataVendorGroup[]

    constructor(
        public name: string,
        public slug: string,
        groupArrays: ManualDataVendorGroupArray[],
        public vendorMaps: string[],
        public vendorTags: string[]
    )
    {
        this.groups = groupArrays.map((groupArray) => new ManualDataVendorGroup(...groupArray))
    }
}
export type ManualDataVendorCategoryArray = ConstructorParameters<typeof ManualDataVendorCategory>

export class ManualDataVendorGroup {
    public sells: ManualDataVendorItem[]
    public sellsFiltered: ManualDataVendorItem[]
    public stats: UserCount

    constructor(
        public name: string,
        itemArrays: ManualDataVendorItemArray[],
        public auto?: boolean
    )
    {
        this.sells = itemArrays.map((itemArray) => new ManualDataVendorItem(...itemArray))
    }
}
export type ManualDataVendorGroupArray = ConstructorParameters<typeof ManualDataVendorGroup>

export class ManualDataVendorItem {
    public costs: Record<number, number>
    public extraAppearances: number
    public faction: Faction = Faction.Both
    public sortedCosts: [string, number, string, number, number][]

    constructor(
        public id: number,
        public type: RewardType,
        public subType: number,
        public quality: ItemQuality,
        public classMask: number,
        costArrays?: number[][],
        public reputation?: number[],
        public appearanceIds?: number[],
        public bonusIds?: number[],
        public note?: string
    )
    {
        this.costs = {}
        if (costArrays) {
            for (const costArray of costArrays) {
                this.costs[costArray[0]] = costArray[1]
            }
        }
    }
    
    getNote(manualData: ManualData, staticData: StaticData): string | undefined {
        return this.costs ? getCurrencyCostsString(manualData, staticData, this.costs, this.reputation) : this.note
    }
}
export type ManualDataVendorItemArray = ConstructorParameters<typeof ManualDataVendorItem>
