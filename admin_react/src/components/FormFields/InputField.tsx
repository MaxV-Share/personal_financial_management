import {
  FormControl,
  FormHelperText,
  InputLabel,
  OutlinedInput,
} from "@mui/material";
import { InputHTMLAttributes } from "react";
import { Control, useController } from "react-hook-form";

export interface InputFieldProps extends InputHTMLAttributes<HTMLInputElement> {
  name: string;
  control: Control<any>;
  label?: string;
}

export function InputField({
  name,
  control,
  label,
  ...inputProps
}: InputFieldProps) {
  const {
    field: { value, onChange, onBlur, ref, name: fieldName },
    fieldState: { invalid, error },
  } = useController({
    name,
    control,
  });
  return (
    <FormControl
      fullWidth
      variant="outlined"
      error={invalid}
      size="small"
      margin="dense"
    >
      <InputLabel htmlFor="outlined-adornment-amount">{label}</InputLabel>
      <OutlinedInput
        id="outlined-adornment-amount"
        // startAdornment={<InputAdornment position="start">$</InputAdornment>}
        // labelWidth={60}
        fullWidth
        value={value}
        name={fieldName}
        onChange={onChange}
        onBlur={onBlur}
        label={label}
        inputRef={ref}
        inputProps={inputProps}
      />
      <FormHelperText id="component-error-text">
        {error?.message}
      </FormHelperText>
    </FormControl>
  );
}
