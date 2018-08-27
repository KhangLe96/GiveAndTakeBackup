import React, { PureComponent } from 'react';
import { Form, Icon, Input, Button, Checkbox } from 'antd';
import { connect } from 'dva';
import { Link } from 'dva/router';

const FormItem = Form.Item;

@connect(({ passport, global }) => ({
  ...passport, ...global,
})) @Form.create()
export default class VerificationCode extends PureComponent {
  state = {
    isSendVerificationCode: false,
    verificationCode: null,
  }

  handleSubmit = (e) => {
    const { dispatch } = this.props;
    e.preventDefault();
    this.props.form.validateFields((err, values) => {
      if (!err) {
        dispatch({
          type: 'passport/verificationCode',
          payload: {
            cmsVerificationCode: values.cmsVerificationCode,
          },
        });
      }
    });
  }

  validateVerificationCode = (e) => {
    this.setState({ cmsVerificationCode: e.target.value });
    this.validateForm();
  }

  validateForm = () => {
    if (this.state.cmsVerificationCode && this.state.cmsVerificationCode.length > 0) {
      this.setState({ isSendVerificationCode: true });
    }
    else {
      this.setState({ isSendVerificationCode: false });
    }
  }

  render() {
    const { getFieldDecorator } = this.props.form;
    return (
      <Form onSubmit={this.handleSubmit} className="login-form">
        <div className="content_style">
          <div className="content_center ">
            Nhập mã xác thực
          </div>
        </div>

        <span>
          Mã xác thực
        </span>
        <FormItem>
          {getFieldDecorator('verificationCode', {
            rules: [{ required: true, message: 'Vui lòng nhập mã xác thực!' }],
          })(
            <Input onChange={this.validateVerificationCode} />,
          )}
        </FormItem>

        <div className="submit_button_row">
          <Button type="primary" htmlType="submit" className="login-form-button" disabled={!this.state.isSendVerificationCode}>
            Xác nhận
          </Button>

          <Button type="primary" className="reset-password-form-button">
            <Link to="/auth/forgot-password">
              Quay lại
            </Link>
          </Button>
        </div>
      </Form>
    );
  }
}
