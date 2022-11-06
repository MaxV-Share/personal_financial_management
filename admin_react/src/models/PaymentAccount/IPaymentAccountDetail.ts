import { ITransactionPerDateModel } from "../Transaction/ITransactionPerDateModel";
import { IPaymentAccountModel } from "./IPaymentAccountModel";

export interface IPaymentAccountDetail extends IPaymentAccountModel {
  transactions: ITransactionPerDateModel[];
}
