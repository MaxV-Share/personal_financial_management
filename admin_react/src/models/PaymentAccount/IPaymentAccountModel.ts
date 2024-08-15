import { ICurrencyModel } from "../Currency";
import { IPaymentAccountTypeModel } from "../PaymentAccountType/IPaymentAccountTypeModel";
import { IPaymentAccountCreateOrUpdateModel } from "./Requests/IPaymentAccountCreateOrUpdateModel";

export interface IPaymentAccountModel
  extends IPaymentAccountCreateOrUpdateModel {
  availableBalance?: number;
  currentBalance?: number;
  currency?: ICurrencyModel;
  type?: IPaymentAccountTypeModel;
}
