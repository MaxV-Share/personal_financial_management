import { PaginationParams } from "./PaginationParams";

export interface ListResponse<T> {
  data: T[];
  pagination: PaginationParams;
}