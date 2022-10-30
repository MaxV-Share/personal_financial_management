import { IBaseTransactionModel } from "./IBaseTransactionModel";
export interface ITransactionItemModel extends IBaseTransactionModel {
  transactionDate?: Date;
  isReport?: boolean;
  imageUrl?: string;
  fromPaymentAccountId?: string;
  toPaymentAccountId?: string;
  categoryId?: string;
}
