import { ITransactionItemModel } from "./ITransactionItemModel";

export interface ITransactionPerDateModel {
  date?: string;
  totalRevenue?: number;
  totalExpense?: number;
  transactions?: ITransactionItemModel[];
}
