import React from 'react';
import { connect } from 'dva';
import { Form, Button, Checkbox, Input } from 'antd';
import styles from './Login.less';

const FormItem = Form.Item;
const { PureComponent } = React;

@connect(({ passport, global, modals, dispatch }) => ({
  ...passport, ...global, ...modals,
}))

@Form.create()
export default class LoginFormModal extends PureComponent {
  state = {
    loginRequest: null,
    login: null,
    password: null,
    isLoggedIn: false,
    dispatch: null,
    loginErrors: {},
    loadFacebookLogin: false,
  }

  componentDidMount() {
    this.props.dispatch({
      type: 'passport/saveLoginError',
      payload: {},
    });
  }

  componentWillReceiveProps(nextProps) {

  }

  dispatchChooseAccountTypeModal = () => {
    this.props.dispatch(
      {
        type: 'modals/show',
        payload: {
          component: 'register-teacher',
        },
      },
    );
  };

  handleSubmit = (e) => {
    const { dispatch } = this.props;
    e.preventDefault();

    this.props.form.validateFields((err, values) => {
      if (!err) {
        const loginRequest = {
          login: values.login,
          password: values.password,
        };
        this.setState({ loginRequest });
        dispatch({
          type: 'passport/login',
          payload: loginRequest,
        });
      } else {

      }
    });
  }

  render() {
    const { getFieldDecorator } = this.props.form;
    const { loginErrors } = this.props;

    const renderLoginErrors = () => {
      if (loginErrors && loginErrors.message !== undefined) {
        return <div>{loginErrors.message}</div>;
      } else {
        <div />;
      }
    };

    return (
      <Form onSubmit={this.handleSubmit}>
        <div className={styles.login}>
          <div className={styles.title}>
            Đăng nhập
          </div>
          <div className={styles.bottom_title}>
            <span>Bạn chưa có tài khoản?</span>
            <a onClick={this.dispatchChooseAccountTypeModal}>Đăng ký</a>
          </div>
          <div className={styles.error}>
            {renderLoginErrors()}
          </div>
          <div className={styles.input}>
            <FormItem>
              {getFieldDecorator('login', {
                rules: [{
                  required: true, message: 'Vui lòng nhập tên tài khoản!',
                },
                ],
              })(
                <Input size="large" placeholder="Tên tài khoản" />,
              )}
            </FormItem>
          </div>
          <div className={styles.input}>
            <FormItem>
              {getFieldDecorator('password', {
                rules: [{
                  required: true, message: 'Vui lòng nhập mật khẩu!',
                }],
              })(
                <Input size="large" type="password" placeholder="Mật khẩu" />,
              )}
            </FormItem>
          </div>
          <div className={styles.input}>
            <div className={styles.remember_account}>
              <Checkbox>Ghi nhớ tài khoản</Checkbox>
              <div className={styles.forgot_password}>
                <a>
                  Quên mật khẩu?
                </a>
              </div>
            </div>
          </div>
          <div className={styles.input}>
            <Button className={styles.button_login_action} htmlType="submit" size="large" type="primary">Đăng
              nhập
            </Button>
          </div>
        </div>
      </Form>
    );
  }
}
