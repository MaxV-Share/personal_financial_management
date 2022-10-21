import { MaxSortDirection } from '../Common';

export interface ISortDescriptor {
  field: string;
  direction?: MaxSortDirection;
}
