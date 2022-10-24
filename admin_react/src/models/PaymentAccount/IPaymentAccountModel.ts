import { IBaseModel } from "../Bases";
import { ICurrencyModel } from "../Currency";
import { IPaymentAccountTypeModel } from "../PaymentAccountType/IPaymentAccountTypeModel";

export interface IPaymentAccountModel extends IBaseModel<string> {
  name?: string;
  initialMoney?: number;
  description?: string;
  isReport?: boolean;
  icon?: string;
  currency?: ICurrencyModel;
  type?: IPaymentAccountTypeModel;
}
