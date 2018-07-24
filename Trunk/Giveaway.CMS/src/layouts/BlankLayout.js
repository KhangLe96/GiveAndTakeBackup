import React from 'react';
import PropTypes from 'prop-types';
import { Link, Route, Switch, Redirect, routerRedux } from 'dva/router';
import DocumentTitle from 'react-document-title';
import { Icon, Layout, Row, Col } from 'antd';
import styles from './BlankLayout.less';
import { connect } from 'dva';
import MainModal from './MainModal';


const { Header, Sider, Content } = Layout;

class BlankLayout extends React.PureComponent {
  static childContextTypes = {
    location: PropTypes.object,
  }

  componentDidMount() {
    if (localStorage.getItem('currentUser')) {
      this.props.dispatch(routerRedux.push('/dashboard'));
    }
  }

  getChildContext() {
    const { location } = this.props;
    return { location };
  }

  getPageTitle() {
    const { getRouteData, location } = this.props;
    const { pathname } = location;
    let title = 'ChoVaNhan';
    getRouteData('BlankLayout').forEach((item) => {
      if (item.path === pathname) {
        title = `${item.name} - ChoVaNhan`;
      }
    });
    return title;
  }

  render() {
    const { getRouteData } = this.props;

    return (
      <DocumentTitle title={this.getPageTitle()}>
        <div className={styles.container}>
          <Layout>
            <div className={styles.top}>
              <div className={styles.header}>
                <MainModal />
              </div>
            </div>
            <div className="content_style">
              <image className="login_image" align="middle" />
            </div>
            <Content>
              <Row type="flex" justify="center" align="center" style={{ position: 'absolute', right: 0, left: 0, alignItems: 'center' }}>
                <Switch>
                  {
                    getRouteData('BlankLayout').map(item =>
                      (
                        <Route
                          exact={item.exact}
                          key={item.path}
                          path={item.path}
                          component={item.component}
                        />
                      ),
                    )
                  }
                  <Redirect exact from="/auth" to="/auth/login" />
                </Switch>
              </Row>
            </Content>
          </Layout>
        </div>
      </DocumentTitle>
    );
  }
}

export default connect()(BlankLayout);
