import List from "@mui/material/List";
import { ITransactionItemModel } from "src/models/Transaction/ITransactionItemModel";
import TransactionItem from "./TransactionItem";

export interface ITransactionListProps {
  data: ITransactionItemModel[];
}

export default function TransactionList(props: ITransactionListProps) {
  const { data } = props;
  return (
    <List component="nav" aria-label="mailbox folders">
      {data.map((item) => (
        <TransactionItem key={item.id} data={item} />
      ))}
    </List>
  );
}
