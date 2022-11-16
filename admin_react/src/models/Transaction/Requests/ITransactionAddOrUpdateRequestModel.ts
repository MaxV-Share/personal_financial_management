import { IPaymentAccountSelectModel } from "../../PaymentAccount/IPaymentAccountSelectModel";
import { IBaseTransactionModel } from "../IBaseTransactionModel";

export interface ITransactionAddOrUpdateRequestModel
  extends IBaseTransactionModel {
  transactionDate?: string;
  isReport?: boolean;
  imageUrl?: string;
  fromPaymentAccountId?: string;
  fromPaymentAccount?: IPaymentAccountSelectModel;
  toPaymentAccountId?: string;
  toPaymentAccount?: IPaymentAccountSelectModel;
  categoryId?: string;
}
