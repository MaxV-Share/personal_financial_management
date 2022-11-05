import { ITransactionItemModel } from "./ITransactionItemModel";

export interface ITransactionPerDateModel {
  date?: Date;
  totalRevenue?: number;
  totalExpense?: number;
  transactions?: ITransactionItemModel[];
}
