import React                        from 'react';
import { Input, Tooltip }           from 'antd';
import { parse, format, asYouType } from 'libphonenumber-js';

function formatNumber(value) {
  const val = `${value}`;
  const list = val.split('.');
  const prefix = list[0].charAt(0) === '-' ? '-' : '';
  let num = prefix ? list[0].slice(1) : list[0];
  let result = '';
  while (num.length > 3) {
    result = `,${num.slice(-3)}${result}`;
    num = num.slice(0, num.length - 3);
  }
  if (num) {
    result = num + result;
  }
  return `${prefix}${result}${list[1] ? `.${list[1]}` : ''}`;
}

export default class PhoneNumber extends React.Component {
  onChange = (e) => {
    const { value } = e.target;
    const reg = /^-?(0[0-9]*)?$/;
    if ((!isNaN(value) && reg.test(value)) || value === '' || value === '-') {
      const secondRegex = /^0(1\d{9}|9\d{8})$/;
      this.props.onChange(value);
    }
  };
  // '.' at the end or only '-' in the input box.
  onBlur = () => {
    const { value, onBlur, onChange } = this.props;
    if (!value) { return; }

    if (value.charAt(value.length - 1) === '.' || value === '-') {
      onChange({ value: value.slice(0, -1) });
    }
    if (onBlur) {
      onBlur();
    }
  };

  render() {
    const { value } = this.props;
    const title = value ? (
      <span className="numeric-input-title">
        {value !== '-' ? formatNumber(value) : '-'}
      </span>
    ) : this.props.placeholder;
    return (
      <Input
        {...this.props}
        onChange={this.onChange}
        onBlur={this.onBlur}
        placeholder={this.props.placeholder}
        pattern=".{8,14}"
        title="Số điện thoại nằm trong khoảng từ 8 đến 12 ký tự!"
      />
    );
  }
}
