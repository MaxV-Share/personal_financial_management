import { IBaseModel } from "../Bases";
import { ICurrencyModel } from "../Currency";
import { IPaymentAccountTypeModel } from "../PaymentAccountType";

export interface IPaymentAccountCreateOrUpdateModel extends IBaseModel<string> {
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
