import { IPagination } from "../Bases";
import { ITransactionCategoryModel } from "./ITransactionCategoryModel";
export interface ITransactionCategoryTable {
  data: ITransactionCategoryModel[];
  pagination: IPagination;
}
