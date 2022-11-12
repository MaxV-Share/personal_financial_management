import { Divider, Typography } from "@mui/material";
import Accordion from "@mui/material/Accordion";
import AccordionDetails from "@mui/material/AccordionDetails";
import AccordionSummary from "@mui/material/AccordionSummary";
import { useTheme } from "@mui/material/styles";
import { Box } from "@mui/system";
import dayjs from "dayjs";
import "dayjs/locale/vi";
import { ITransactionPerDateModel } from "src/models/Transaction/ITransactionPerDateModel";
import TransactionList from "./TransactionList";
export interface ITransactionPerDateProps {
  id: string;
  data: ITransactionPerDateModel;
}

export default function TransactionPerDate(props: ITransactionPerDateProps) {
  const { data } = props;
  const { id } = props;
  const theme = useTheme();
  let vndLocale = Intl.NumberFormat("vi-VN");
  return (
    <Accordion expanded={true}>
      <AccordionSummary
        // expandIcon={<ExpandMoreIcon />}
        aria-controls={`${id}-content`}
        id={`${id}-header`}
      >
        <Box
          sx={{
            display: "flex",
            justifyContent: "space-between",
            alignItems: "center",
            bgcolor: "background.paper",
            borderRadius: 1,
            width: "100%",
          }}
        >
          <Box>
            <Typography>
              {data?.date == null
                ? dayjs().format("DD/MM/YYYY") + " demo"
                : dayjs(data.date).locale("vi").format()}
            </Typography>
          </Box>

          <Box>
            <Typography color="success.main" align="right">
              {vndLocale.format(data?.totalRevenue ?? 0)}
            </Typography>
            <Typography color="error" align="right">
              {vndLocale.format(data?.totalExpense ?? 0)}
            </Typography>
          </Box>
        </Box>
      </AccordionSummary>
      <AccordionDetails>
        <Divider />
        <TransactionList data={data?.transactions ?? []} />
      </AccordionDetails>
    </Accordion>
  );
}
