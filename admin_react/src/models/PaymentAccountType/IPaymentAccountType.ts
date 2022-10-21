import { IBaseModel } from "../Bases";

export interface IPaymentAccountType extends IBaseModel<string> {
  /**
   * The payment account code identifier
   */
  code: string;

  /**
   * The payment account name
   */
  name: string;
}
