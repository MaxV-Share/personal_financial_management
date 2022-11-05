import Autocomplete from "@mui/material/Autocomplete";
import TextField from "@mui/material/TextField";
import { Control, Controller } from "react-hook-form";
import { IPaymentAccountSelectModel } from "src/models/PaymentAccount/IPaymentAccountSelect";

// Top 100 films as rated by IMDb users. http://www.imdb.com/chart/top
export interface IPaymentAccountAutocompleteProps {
  control: Control<any>;
  id: string;
  name: string;
  label: string;
  startAdornment?: React.ReactNode;
  optionsData: IPaymentAccountSelectModel[];
}

export default function PaymentAccountAutocomplete({
  id,
  control,
  name,
  label,
  optionsData,
  startAdornment,
}: IPaymentAccountAutocompleteProps) {
  return (
    <Controller
      render={(props) => (
        <Autocomplete
          id={name}
          // sx={{ width: 300 }}
          options={optionsData}
          getOptionLabel={(option: IPaymentAccountSelectModel) => option.name}
          renderInput={(params) => (
            <TextField
              {...params}
              label={label}
              margin="normal"
              InputProps={{
                ...params.InputProps,
                startAdornment: startAdornment,
              }}
            />
          )}
          filterOptions={(options: IPaymentAccountSelectModel[], object) => {
            return options.filter(
              (e) =>
                e?.name == object.inputValue ||
                object.inputValue === undefined ||
                object.inputValue == "" ||
                e.name
                  .toLocaleLowerCase()
                  .indexOf(object.inputValue.toLocaleLowerCase()) >= 0
            );
          }}
          renderOption={(props, option, { inputValue }) => {
            // const matches = match(option.title, inputValue);
            // const parts = parse(option.title, matches);

            return (
              <li {...props}>
                <div>{option.name}</div>
              </li>
            );
          }}
          onChange={(_, value) => {
            return props.field.onChange(value);
          }}
        />
      )}
      name={name}
      control={control}
    />
  );
}
