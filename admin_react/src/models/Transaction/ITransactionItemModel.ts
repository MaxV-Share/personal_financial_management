import { IBaseTransactionModel } from "./IBaseTransactionModel";
export interface ITransactionItemModel extends IBaseTransactionModel {
  transactionDate?: Date;
  isReport?: boolean;
  imageUrl?: string;
  balance?: number;
  fromPaymentAccountId?: string;
  toPaymentAccountId?: string;
  categoryId?: string;
}
