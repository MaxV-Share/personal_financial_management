import { IconButton, Tooltip, useTheme } from "@mui/material";
import Paper from "@mui/material/Paper";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TablePagination from "@mui/material/TablePagination";
import TableRow from "@mui/material/TableRow";
import * as React from "react";
import { useEffect } from "react";
import { useAppDispatch, useAppSelector } from "src/app/hooks";
import { selectTransactionCategoryTable } from "../transactionCategorySlice";
// import { selectTransactionCategoryTable } from "../currencySlice";
import DeleteTwoToneIcon from "@mui/icons-material/DeleteTwoTone";
import EditTwoToneIcon from "@mui/icons-material/EditTwoTone";
import { useNavigate } from "react-router";
import { ITransactionCategoryModel } from "../../../models/TransactionCategory/ITransactionCategoryModel";

interface Column {
  id:
    | "id"
    | "name"
    | "code"
    | "description"
    | "parentId"
    | "parentName"
    | "parentDescription";
  label: string;
  minWidth?: number;
  align?: "right";
  format?: (value: number) => string;
}

const columns: Column[] = [
  { id: "id", label: "Id" },
  { id: "name", label: "Name" },
  { id: "description", label: "Description" },
  { id: "code", label: "ISO\u00a0Code" },
  { id: "parentId", label: "parent\u00a0Id" },
  { id: "parentName", label: "parent\u00a0Name" },
  { id: "parentDescription", label: "parent\u00a0Description" },
];
export interface TransactionCategoryTableRowProps {
  data: ITransactionCategoryModel;
  id?: any;
}
export function TransactionCategoryTableRow(
  props: TransactionCategoryTableRowProps
) {
  const { data, id } = props;
  const theme = useTheme();
  const navigate = useNavigate();
  return (
    <TableRow hover role="checkbox" tabIndex={-1} key={id}>
      {columns.map((column) => {
        const value = data[column.id];
        return (
          <TableCell key={column.id + id} align={column.align}>
            {column.format && typeof value === "number"
              ? column.format(value)
              : value}
          </TableCell>
        );
      })}
      <TableCell align="right" key={"actions" + id}>
        <Tooltip title="Edit TransactionCategory" arrow>
          <IconButton
            sx={{
              "&:hover": {
                background: theme.colors.primary.lighter,
              },
              color: theme.palette.primary.main,
            }}
            color="inherit"
            size="small"
            onClick={() => navigate(`update/${data.id}`)}
          >
            <EditTwoToneIcon fontSize="small" />
          </IconButton>
        </Tooltip>
        <Tooltip title="Delete TransactionCategory" arrow>
          <IconButton
            sx={{
              "&:hover": {
                background: theme.colors.error.lighter,
              },
              color: theme.palette.error.main,
              ml: 0.5,
            }}
            color="inherit"
            size="small"
          >
            <DeleteTwoToneIcon fontSize="small" />
          </IconButton>
        </Tooltip>
      </TableCell>
    </TableRow>
  );
}

export interface ITransactionCategoryTableProps {}

export default function TransactionCategoryTable(
  props: ITransactionCategoryTableProps
) {
  const [page, setPage] = React.useState(0);
  const [rowsPerPage, setRowsPerPage] = React.useState(10);
  const dispatch = useAppDispatch();

  const handleChangePage = (event: unknown, newPage: number) => {
    setPage(newPage);
  };
  const tableTransactionCategories = useAppSelector(
    selectTransactionCategoryTable
  );
  useEffect(() => {
    console.log("tableTransactionCategories", tableTransactionCategories);
  }, [dispatch, tableTransactionCategories]);
  const handleChangeRowsPerPage = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setRowsPerPage(+event.target.value);
    setPage(0);
  };
  const theme = useTheme();

  return (
    <Paper sx={{ width: "100%" }}>
      <TableContainer>
        <Table stickyHeader aria-label="sticky table">
          <TableHead>
            <TableRow key="headerTable1">
              <TableCell align="center" colSpan={columns.length + 1}>
                TransactionCategory Table
              </TableCell>
            </TableRow>
            <TableRow key="headerTable2">
              {columns.map((column) => (
                <TableCell
                  key={column.id}
                  align={column.align}
                  style={{ top: 57, minWidth: column.minWidth }}
                >
                  {column.label}
                </TableCell>
              ))}
              <TableCell
                align="right"
                style={{ top: 57, maxWidth: 150, width: 150 }}
                key="actions"
              >
                Actions
              </TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {tableTransactionCategories?.data?.map((row) => {
              return (
                <>
                  <TransactionCategoryTableRow
                    data={row}
                    id={row.id}
                    key={row.id}
                  />
                  {row.childrens.map((rowChild) => {
                    return (
                      <TransactionCategoryTableRow
                        id={rowChild.parentId + rowChild.id}
                        key={rowChild.parentId + rowChild.id}
                        data={rowChild}
                      />
                    );
                  })}
                </>
              );
            })}
          </TableBody>
        </Table>
        <TablePagination
          rowsPerPageOptions={[10, 25, 100, { value: -1, label: "All" }]}
          component="div"
          count={tableTransactionCategories.pagination.totalRows}
          rowsPerPage={tableTransactionCategories.pagination.pageSize}
          page={tableTransactionCategories.pagination.pageIndex - 1}
          onPageChange={handleChangePage}
          onRowsPerPageChange={handleChangeRowsPerPage}
        />
      </TableContainer>
    </Paper>
  );
}
