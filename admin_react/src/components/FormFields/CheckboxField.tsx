import { Checkbox, FormControlLabel } from "@mui/material";
import { Control, Controller, useController } from "react-hook-form";

export interface RadioOption {
  label?: string;
  value: number | string;
}

export interface CheckboxFieldProps {
  name: string;
  control: Control<any>;
  label?: string;
  disabled?: boolean;
  options: RadioOption[];
  defaultChecked: boolean;
}

export function CheckboxField({
  name,
  control,
  label,
  disabled,
  options,
  defaultChecked,
}: CheckboxFieldProps) {
  const {
    field: { value, onChange, onBlur },
    fieldState: { invalid, error },
  } = useController({
    name,
    control,
  });

  return (
    // <FormControl disabled={disabled} margin="normal" error={invalid}>
    //   <FormLabel component="legend">{label}</FormLabel>

    //   {/* <Checkbox
    //     name={name}
    //     value={value}
    //     onChange={onChange}
    //     onBlur={onBlur}
    //     defaultChecked={defaultChecked}
    //   /> */}
    //   <FormControlLabel
    //     control={<Checkbox checked={true} onChange={onChange} name="jason" />}
    //     label="Jason Killian"
    //   />

    //   <FormHelperText>{error?.message}</FormHelperText>
    // </FormControl>
    <FormControlLabel
      control={
        <Controller
          name={name}
          control={control}
          render={({ field: props }) => (
            <Checkbox
              {...props}
              disabled={disabled}
              checked={props.value}
              onChange={(e) => props.onChange(onChange)}
            />
          )}
        />
      }
      label={label}
    />
  );
}
