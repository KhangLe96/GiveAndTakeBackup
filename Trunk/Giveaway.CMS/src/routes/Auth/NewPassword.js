import React, { PureComponent } from 'react';
import { Form, Input, Button } from 'antd';
import { connect } from 'dva';
import { Link } from 'dva/router';

const FormItem = Form.Item;


@connect(({ passport, global }) => ({
  ...passport, ...global,
})) @Form.create()
export default class NewPassword extends PureComponent {
  state = {
    isPasswordMatched: false,
    confirmDirty: false,
    newPassword: "",
    confirmPassword: "",
  }
  handleSubmit = (e) => {
    const { dispatch } = this.props;
    e.preventDefault();
    this.props.form.validateFields((err, values) => {
      if (!err) {
        dispatch({
          type: 'passport/newPassword',
          payload: {
            newPassword: values.newPassword,
            confirmPassword: values.confirmPassword,
          },
        });
      }
    });
  }
  checkPassword = (rule, value, callback) => {
    const form = this.props.form;
    if (value && value !== form.getFieldValue('newPassword')) {
      callback('Xác nhận mật khẩu không chính xác!');
    } else {
      callback();
      const confirmPassword = value;
      const newPassword = this.props.form.getFieldValue('newPassword');
      this.validateForm(newPassword, confirmPassword);
    }
  }
  checkConfirm = (rule, value, callback) => {
    const form = this.props.form;
    if (value && this.state.confirmDirty) {
      form.validateFields(['confirmPassword'], { force: true });
    }
    else {
      callback();
      const newPassword = value;
      const confirmPassword = this.props.form.getFieldValue('confirmPassword');
      this.validateForm(confirmPassword, newPassword);
    }
  }
  validateForm = (newPassword, confirmPassword) => {
    if (newPassword && confirmPassword
      && newPassword.length > 5 && confirmPassword.length > 5
      && newPassword === confirmPassword) {
      this.setState({ isPasswordMatched: true });
    }
    else {
      this.setState({ isPasswordMatched: false });
    }
  }

  render() {
    const { getFieldDecorator } = this.props.form;

    return (
      <Form onSubmit={this.handleSubmit} className="login-form">
        <div className="content_style">
          <div className="content_center ">
            Tạo mật khẩu mới
          </div>
        </div>

        <span>Mật khẩu mới</span>
        <FormItem>
          {getFieldDecorator('newPassword', {
            rules: [{
              required: true, message: 'Vui lòng nhập mật khẩu mới!',
            }, {
              validator: this.checkConfirm,
            },
            ],
          })(
            <Input size="large" type="password" maxLength="40" placeholder="Mật khẩu gồm 6 kí tự" />,
          )}
        </FormItem>

        <span>Xác nhận mật khẩu</span>
        <FormItem>
          {getFieldDecorator('newPasswordRetype', {
            rules: [{
              required: true, message: 'Vui lòng nhập lại mật khẩu mới!',
            }, {
              validator: this.checkPassword,
            },
            ],
          })(
            <Input size="large" type="password" maxLength="50" placeholder="Mật khẩu gồm 6 kí tự" />,
          )}
        </FormItem>

        <div className="submit_button_row">
          <Button type="primary" htmlType="submit" className="login-form-button" disabled={!this.state.isPasswordMatched}>
            Xác nhận
          </Button>

          <Button type="primary" className="reset-password-form-button">
            <Link to="/auth/verification-code">
              Quay lại
            </Link>
          </Button>
        </div>
      </Form>
    );
  }
}
