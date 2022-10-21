import { FilterType } from '../Common/FilterType';
export interface IFilterDetailsRequest {
  attributeName: string,
  value: string,
  filterType: FilterType,
}