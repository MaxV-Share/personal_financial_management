import { ICurrencyModel } from "../Currency";
import { IPaymentAccountTypeModel } from "../PaymentAccountType/IPaymentAccountTypeModel";
import { IPaymentAccountCreateOrUpdateRequestModel } from "./Requests/IPaymentAccountCreateOrUpdateRequestModel";

export interface IPaymentAccountModel
  extends IPaymentAccountCreateOrUpdateRequestModel {
  availableBalance?: number;
  currentBalance?: number;
  currency?: ICurrencyModel;
  type?: IPaymentAccountTypeModel;
}
