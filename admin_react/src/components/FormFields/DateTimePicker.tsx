// import { InputOutlined } from "@mui/icons-material";
import {
  FormControl,
  FormHelperText,
  InputLabel,
  OutlinedInput,
} from "@mui/material";
import * as React from "react";
import { InputHTMLAttributes } from "react";
import { Control, useController } from "react-hook-form";
export interface InputFieldProps extends InputHTMLAttributes<HTMLInputElement> {
  name: string;
  control: Control<any>;
  label?: string;
  id: string;
  startAdornment?: React.ReactNode;
  endAdornment?: React.ReactNode;
  //   defaultValue?: any;
}

export function DateTimePicker({
  name,
  control,
  label,
  disabled,
  id,
  startAdornment,
  endAdornment,
  //   defaultValue,
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
      size="medium"
      margin="dense"
      disabled={disabled}
    >
      <InputLabel htmlFor={id}>{label}</InputLabel>
      <OutlinedInput
        id={id}
        startAdornment={startAdornment}
        endAdornment={endAdornment}
        // labelWidth={60}
        fullWidth
        value={value}
        name={fieldName}
        onChange={onChange}
        onBlur={onBlur}
        label={label}
        inputRef={ref}
        type="datetime-local"
        inputProps={inputProps}
      ></OutlinedInput>
      {/* <TextField
        id={id}
        label={"dsds"}
        sx={{ width: "100%" }}
        InputLabelProps={{
          shrink: true,
        }}
        inputProps={inputProps}
      /> */}
      <FormHelperText id="component-error-text">
        {error?.message}
      </FormHelperText>
    </FormControl>
  );
}
