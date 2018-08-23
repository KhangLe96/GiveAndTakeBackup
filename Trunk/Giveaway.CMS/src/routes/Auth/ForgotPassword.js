import React, { PureComponent } from 'react';
import { Form, Input, Button, message } from 'antd';
import { connect } from 'dva';
import { Link } from 'dva/router';

const FormItem = Form.Item;
const formItemLayout = {
  labelCol: {
    xs: { span: 24, },
    sm: { span: 4 },
  },
  wrapperCol: {
    xs: { span: 24 },
    sm: { span: 20 },
  },
};

@connect(({ passport, global }) => ({
  ...passport, ...global,
}))
@Form.create()
export default class ForgotPassword extends PureComponent {
  state = {
    isSendCodeToEmail: false,
    email: null,
  }
  
  componentDidMount(){
    message.info(this.props.match.params.id);
  }

  handleSubmit = (e) => {
    const { dispatch } = this.props;
    e.preventDefault();
    this.props.form.validateFields((err, values) => {
      if (!err) {
        dispatch({
          type: 'passport/forgotPassword',
          payload: {
            email: values.email,
          },
        });
      }
    });
  }

  validateEmail = (e) => {
    this.setState({ email: e.target.value });
    this.validateForm();
  }

  validateForm = () => {
    let error = this.props.form.getFieldError('email')
    this.setState({ isSendCodeToEmail: !error });
  }

  render() {
    const { getFieldDecorator } = this.props.form;

    return (

      <Form onSubmit={this.handleSubmit} className="login-form">
        <div className="content_style">
          <div className="content_center ">
            Nhận mã xác minh
          </div>
        </div>

        <FormItem
          {...formItemLayout}
          label="E-mail" className={'pull-left'}>
          {getFieldDecorator('email', {
            rules: [{
              type: 'email', message: 'Địa chỉ mail không hợp lệ.',
            }, {
              required: true, message: 'Nhập địa chỉ mail!',
            }],
          })(
            <Input onChange={this.validateEmail} />,
          )}
        </FormItem>
        <div className="submit_button_row">

          <Button type="primary" htmlType="submit" className="login-form-button" disabled={!this.state.isSendCodeToEmail}>
            Gửi mã xác nhận
          </Button>

          <Button type="primary" htmlType="submit" className="reset-password-form-button">
            <Link to="/auth/login">
              Quay lại
            </Link>
          </Button>
        </div>
      </Form>
    );
  }
}
