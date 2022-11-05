import { ITransactionPerDateModel } from "../Transaction/ITransactionPerDateModel";
import { IPaymentAccountModel } from "./IPaymentAccountModel";

export interface IPaymentAccountTransactionsModel extends IPaymentAccountModel {
  transactions: ITransactionPerDateModel[];
}
