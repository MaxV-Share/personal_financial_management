import { IFilterBodyRequest } from "src/models/Bases";

export interface IBaseTransactionsRequest extends IFilterBodyRequest {
  fromDate?: string;
  toDate?: string;
}
