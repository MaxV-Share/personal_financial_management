import { IPagination } from ".";

export interface IBasePaging<T> {
  pagination: IPagination,
  data: T[]
}