import { ISortDescriptor } from ".";
import { IFilterRequest } from "./IFilterRequest";
import { IPagination } from './IPagination';

export interface IFilterBodyRequest {
  langId: string,
  searchValue?: string,
  filter?: IFilterRequest,
  orders?: ISortDescriptor[],
  pagination: IPagination,

}