import { IBaseModel } from "../Bases";

export interface ICurrencyModel extends IBaseModel<string> {
  /**
   * The currency code identifier
   */
  code: string;

  /**
   * The currency name
   */
  name: string;

  /**
   * The currency icon
   */
  icon: string;
}
