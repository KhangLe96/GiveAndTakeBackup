import React, { PureComponent } from 'react';
import { Alert, Button, Form, Icon, Input, Spin } from 'antd';
import { connect } from 'dva';
import { GoogleLogin } from 'react-google-login-component';
import { Link } from 'dva/router';


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
            login: values.userName,
          },
        });
      }
    });
  };

  validateUsername = (e) => {
    this.setState({ userName: e.target.value });
    this.validateForm();
  };

  validatePassword = (e) => {
    this.setState({ password: e.target.value });
    this.validateForm();
  };

  validateForm = () => {
    if (this.state.userName && this.state.password && this.state.userName.length > 3 && this.state.password.length > 6) {
      this.setState({ isLoginEnable: true });
    }
  };

  render() {
    const { getFieldDecorator } = this.props.form;
    const { loginErrors } = this.props;

    const renderError = () => {
      if (loginErrors.message && loginErrors.type) {
        return (<div style={{ margin: '25px 0' }}>
          <Alert message={loginErrors.message} type="error" />
        </div>);
      }
    };

    return (
      <Spin spinning={this.props.loading}>
        <div className="login_container">
          <Form onSubmit={this.handleSubmit} className="login-form">
            <div className="content_center ">
              <img
                style={{ width: '400px', height: '135px', zIndex: '10', marginBottom: '50px' }}
                src="/images/img_logo_login.png"
              />
            </div>
            {renderError()}
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
            <div className="login_facebook_row">
              <div className="fb-login-button" data-width="398" data-max-rows="1" data-size="large"
                   data-button-type="login_with" data-show-faces="false" data-auto-logout-link="false"
                   data-use-continue-as="false"></div>

            </div>
            <div className="login_facebook_row">
              <div className="g-signin2" data-onsuccess="onSignIn" data-width="398" data-height="200" data-max-rows="1" data-longtitle="true"></div>
            </div>
          </Form>
        </div>
      </Spin>
    );
  }
}
