import { IBaseTransactionsRequest } from "./IBaseTransactionsRequest";

export interface IFetchTransactionsByPaymentAccountRequest
  extends IBaseTransactionsRequest {
  paymentAccountId?: string;
}
