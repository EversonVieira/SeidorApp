import { FilterAggregatenType } from "../Enums/filter-aggregaten-type";
import { FilterOperationType } from "../Enums/filter-operation-type";

export class Filter {
    filterGroup:number = 0;
    operationType:FilterOperationType = FilterOperationType.Equals;
    aggregateType:FilterAggregatenType = FilterAggregatenType.AND; 
    target1:string = '';
    target2:string = '';
    value1:any = null;
    value2:any = null;
}
