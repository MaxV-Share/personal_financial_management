import { Divider } from "@mui/material";
import List from "@mui/material/List";
import TransactionItem from "./TransactionItem";

export interface ITransactionListProps {}

export default function TransactionList(props: ITransactionListProps) {
  return (
    <>
      <List component="nav" aria-label="mailbox folders">
        <TransactionItem />
        <Divider />
        <TransactionItem />
        <Divider />
        <TransactionItem />
        <Divider />
      </List>
    </>
  );
}
