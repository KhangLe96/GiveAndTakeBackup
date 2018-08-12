import React, { PureComponent } from 'react';
import { Alert, Button, Form, Icon, Input, Spin } from 'antd';
import { connect } from 'dva';
import { GoogleLogin } from 'react-google-login-component';
import { Link } from 'dva/router';
import styles from './Login.less';
import cx from 'classnames';

const FormItem = Form.Item;

@connect(({ passport, global, modals }) => ({
  ...passport, ...global, ...modals,
}))
@Form.create()
export default class Login extends PureComponent {
  state = {
    userName: null,
    password: null,
  };

  componentDidMount() {
  }

  handleSubmit = (e) => {
    const { dispatch } = this.props;
    e.preventDefault();
    this.props.form.validateFields((err, values) => {
      if (!err) {
        dispatch({
          type: 'passport/login',
          payload: {
            password: values.password,
            username: values.userName,
          },
        });
      }
    });
  };

  validateUsername = (e) => {
    //
    this.setState({ userName: e.target.value });
    this.validateForm();
  };

  validatePassword = (e) => {
    this.setState({ password: e.target.value });
    this.validateForm();
  };

  validateForm = () => {

  };

  render() {
    const { getFieldDecorator } = this.props.form;

    return (
      <Spin spinning={this.props.loading}>
        <div className="login_container">
          <Form onSubmit={this.handleSubmit} className="login-form">
            <div className={cx('content_center')}>
              <img
                style={{ width: '100%', zIndex: '10', marginBottom: '50px' }}
                src="/images/img_logo_login.png"
                alt=""
              />
            </div>
            <span>
              Tên đăng nhập
            </span>
            <FormItem>
              {getFieldDecorator('userName', {
                rules: [{ required: true, message: 'Vui lòng điền tên đăng nhập!' }],
              })(
                <Input
                  prefix={<Icon type="user" style={{ color: 'rgba(0,0,0,.25)' }} />}
                  placeholder="Tên đăng nhập"
                  size="large"
                  onChange={this.validateUsername}
                />,
              )}
            </FormItem>

            <span>
              Mật khẩu
            </span>
            <FormItem>
              {getFieldDecorator('password', {
                rules: [{ required: true, message: 'Vui lòng nhập mật khẩu!' }],
              })(
                <Input
                  prefix={<Icon type="lock" style={{ color: 'rgba(0,0,0,.25)' }} />}
                  type="password"
                  size="large"
                  placeholder="Mật khẩu"
                  onChange={this.validatePassword}
                />,
              )}
            </FormItem>
            <div className="submit_button_row">
              <Button type="primary" size="large" htmlType="submit" className="login-form-button">
                Đăng nhập
              </Button>
            </div>
          </Form>
        </div >
      </Spin >
    );
  }
}
