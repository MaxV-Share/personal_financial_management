export interface ICurrencyCreateOrUpdateRequest {
  /**
   * The currency code identifier
   */
  code?: string;

  /**
   * The currency name
   */
  name?: string;

  /**
   * The currency icon
   */
  icon: string;
}
