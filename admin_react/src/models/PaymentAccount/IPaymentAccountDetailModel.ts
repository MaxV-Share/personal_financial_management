import { ITransactionPerDateModel } from "../Transaction/ITransactionPerDateModel";
import { IPaymentAccountModel } from "./IPaymentAccountModel";

export interface IPaymentAccountDetailModel extends IPaymentAccountModel {
  transactions: ITransactionPerDateModel[];
}
