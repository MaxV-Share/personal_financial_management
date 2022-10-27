import { Box, Divider } from "@mui/material";
import TransactionPerDate from "./TransactionPerDate";

export interface ITransactionListProps {}

export default function TransactionDateList(props: ITransactionListProps) {
  return (
    <>
      {/* <Grid item xs={12}> */}
      <Box mt={1}>
        <TransactionPerDate id={"panel1a"} />
        <Divider />
      </Box>
      <Box mt={1}>
        <TransactionPerDate id={"panel2a"} />
        <Divider />
      </Box>
      <Box mt={1}>
        <TransactionPerDate id={"panel3a"} />
        <Divider />
      </Box>
      s{/* </Grid> */}
    </>
  );
}
