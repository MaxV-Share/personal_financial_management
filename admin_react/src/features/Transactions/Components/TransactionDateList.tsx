import { Box, Divider } from "@mui/material";
import { ITransactionPerDateModel } from "src/models/Transaction/ITransactionPerDateModel";
import TransactionPerDate from "./TransactionPerDate";

export interface ITransactionListProps {
  data: ITransactionPerDateModel[];
}

export default function TransactionDateList(props: ITransactionListProps) {
  const { data } = props;
  return (
    <>
      {/* <Grid item xs={12}> */}
      {data.map((transactionPerDate) => {
        return (
          <Box mt={1} key={transactionPerDate.date}>
            <TransactionPerDate data={transactionPerDate} id={"panel1a"} />
            <Divider />
          </Box>
        );
      })}
      {/* </Grid> */}
    </>
  );
}
