import { ICurrencyModel } from "../Currency";
import { IPaymentAccountTypeModel } from "../PaymentAccountType";
import { IBasePaymentAccountModel } from "./IBasePaymentAccountModel";

export interface IPaymentAccountCreateOrUpdateModel
  extends IBasePaymentAccountModel {
  name: string;
  initialMoney: number;
  creditLimit?: number;
  description?: string;
  isReport: boolean;
  icon?: string;
  order?: number;
  currencyId?: string;
  currency?: ICurrencyModel;
  typeId?: string;
  type?: IPaymentAccountTypeModel;
}
