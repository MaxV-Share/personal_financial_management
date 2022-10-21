import { FilterLogicalOperator } from '../Common';
import { IFilterDetailsRequest } from './IFilterDetailsRequest';

export interface IFilterRequest {
  logicalOperator: FilterLogicalOperator;
  Details: IFilterDetailsRequest[];
}
