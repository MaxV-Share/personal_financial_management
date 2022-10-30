import { IPaymentAccountSelectModel } from "./../PaymentAccount/IPaymentAccountSelect";
import { IBaseTransactionModel } from "./IBaseTransactionModel";

export interface ITransactionAddOrUpdateModel extends IBaseTransactionModel {
  transactionDate?: Date;
  isReport?: boolean;
  imageUrl?: string;
  fromPaymentAccountId?: string;
  fromPaymentAccount?: IPaymentAccountSelectModel;
  toPaymentAccountId?: string;
  toPaymentAccount?: IPaymentAccountSelectModel;
  categoryId?: string;
}
