import React from 'react';
import PropTypes from 'prop-types';
import DocumentTitle from 'react-document-title';

import {connect} from 'dva';
import {Link, Route, Redirect, Switch, routerRedux} from 'dva/router';
import {Popover, Layout, Menu, Icon, Avatar, Dropdown, Tag, message, Spin} from 'antd';
import cx from 'classnames';

import NoticeIcon from 'ant-design-pro/lib/NoticeIcon';
import GlobalFooter from 'ant-design-pro/lib/GlobalFooter';

import moment from 'moment';
import groupBy from 'lodash/groupBy';
import classNames from 'classnames';
import {ContainerQuery} from 'react-container-query';

import styles from './BasicLayout.less';
import SidebarIcon from './SidebarIcon';

import {DEFAULT_AVATAR_URL} from '../common/constants';
import MainModal from './MainModal';

const {Header, Sider, Content} = Layout;
const {SubMenu} = Menu;

const query = {
  'screen-xs': {
    maxWidth: 575,
  },
  'screen-sm': {
    minWidth: 576,
    maxWidth: 767,
  },
  'screen-md': {
    minWidth: 768,
    maxWidth: 991,
  },
  'screen-lg': {
    minWidth: 992,
    maxWidth: 1199,
  },
  'screen-xl': {
    minWidth: 1200,
  },
};

class BasicLayout extends React.PureComponent {
  static childContextTypes = {
    location: PropTypes.object,
    breadcrumbNameMap: PropTypes.object,
  };

  constructor(props) {
    super(props);
    // make 1st level children of Layout to make the menus
    this.menus = props.navData.reduce((arr, current) => arr.concat(current.children), []);
    this.state = {
      openKeys: this.getDefaultCollapsedSubMenus(props),
      currentHoverMenuItemKey: props.currentHoverMenuItemKey,
    };
  }

  getChildContext() {
    const {location, navData, getRouteData} = this.props;
    const routeData = getRouteData('BasicLayout');
    const firstMenuData = navData.reduce((arr, current) => arr.concat(current.children), []);
    const menuData = this.getMenuData(firstMenuData, '');
    const breadcrumbNameMap = {};

    routeData.concat(menuData).forEach((item) => {
      breadcrumbNameMap[item.path] = item.name;
    });
    return {location, breadcrumbNameMap};
  }

  componentDidMount() {
    this.props.dispatch({
      type: 'passport/init',
    });
    this.props.dispatch({
      type: 'passport/fetchCurrentUserFromLocalStore',
    });
  }

  componentWillReceiveProps(nextProps) {
    if (!localStorage.getItem('currentUser')) {
      // this.props.dispatch(routerRedux.push('/auth/login'));
    }
  }

  componentWillUnmount() {
    clearTimeout(this.resizeTimeout);
  }

  onMenuClick = ({key}) => {
    if (key === 'logout') {
      this.props.dispatch({
        type: 'passport/logout',
      });
    }
  };

  getMenuData = (data, parentPath) => {
    let arr = [];
    data.forEach((item) => {
      if (item.children) {
        arr.push({path: `${parentPath}/${item.path}`, name: item.name});
        arr = arr.concat(this.getMenuData(item.children, `${parentPath}/${item.path}`));
      }
    });
    return arr;
  };

  getDefaultCollapsedSubMenus(props) {
    const currentMenuSelectedKeys = [...this.getCurrentMenuSelectedKeys(props)];
    currentMenuSelectedKeys.splice(-1, 1);
    if (currentMenuSelectedKeys.length === 0) {
      return ['dashboard'];
    }
    return currentMenuSelectedKeys;
  }

  getCurrentMenuSelectedKeys(props) {
    const {location: {pathname}} = props || this.props;
    const keys = pathname.split('/').slice(1);
    if (keys.length === 1 && keys[0] === '') {
      return [this.menus[0].key];
    }
    return keys;
  }

  getNavMenuItems(menusData, parentPath = '') {
    if (!menusData) {
      return [];
    }
    return menusData.map((item) => {
      if (!item.name || (item.visible === false)) {
        return null;
      }
      let itemPath;
      if (item.path.indexOf('http') === 0) {
        itemPath = item.path;
      } else {
        itemPath = `${parentPath}/${item.path || ''}`.replace(/\/+/g, '/');
      }
      if (item.children && item.children.some(child => child.name) && item.visibleChildren) {
        return (
          <SubMenu
            title={
              item.icon ? (
                <span>
                  <Icon type={item.icon}/>
                  <span>{item.name}</span>
                </span>
              ) : item.name
            }
            key={item.key || item.path}
          >
            {this.getNavMenuItems(item.children, itemPath)}
          </SubMenu>
        );
      }
      const renderIcon = (fn) => {
        return item.icon && <SidebarIcon name={item.icon} size="2x" state={fn() ? 'hover' : 'normal'}/>;
      };

      return (
        <Menu.Item
          key={item.key || item.path}
          onMouseEnter={(e) => {
            this.setState({...this.state, currentHoverMenuItemKey: item.path});
          }}
        >
          <Link
            to={itemPath}
            target={item.target}
            replace={itemPath === this.props.location.pathname}
            onMouseLeave={e => this.setState({...this.state, currentHoverMenuItemKey: ''})}
          >
            {renderIcon(() => (this.props.location.pathname.indexOf(itemPath) >= 0 || this.state.currentHoverMenuItemKey === item.path))}<span>{item.name}</span>
          </Link>
          )
        </Menu.Item>
      );
    });
  }

  getPageTitle() {
    const {location, getRouteData} = this.props;
    const {pathname} = location;
    let title = 'ChoVaNhan';
    getRouteData('BasicLayout').forEach((item) => {
      if (item.path === pathname) {
        title = `${item.name} - ChoVaNhan`;
      }
    });
    return title;
  }

  getNoticeData() {
    const {notices = []} = this.props;
    if (notices.length === 0) {
      return {};
    }
    const newNotices = notices.map((notice) => {
      const newNotice = {...notice};
      if (newNotice.datetime) {
        newNotice.datetime = moment(notice.datetime).fromNow();
      }
      // transform id to item key
      if (newNotice.id) {
        newNotice.key = newNotice.id;
      }
      if (newNotice.extra && newNotice.status) {
        const color = ({
          todo: '',
          processing: 'blue',
          urgent: 'red',
          doing: 'gold',
        })[newNotice.status];
        newNotice.extra = <Tag color={color} style={{marginRight: 0}}>{newNotice.extra}</Tag>;
      }
      return newNotice;
    });
    return groupBy(newNotices, 'type');
  }

  handleOpenChange = (openKeys) => {
    const lastOpenKey = openKeys[openKeys.length - 1];
    const isMainMenu = this.menus.some(
      item => lastOpenKey && (item.key === lastOpenKey || item.path === lastOpenKey),
    );
    this.setState({
      openKeys: isMainMenu ? [lastOpenKey] : [...openKeys],
    });
  };

  handleNoticeClear = (type) => {
    message.success(`Đã xóa ${type}`);
    this.props.dispatch({
      type: 'global/clearNotices',
      payload: type,
    });
  };

  handleNoticeVisibleChange = (visible) => {
    if (visible) {
      this.props.dispatch(routerRedux.push('/notification'));
    }
  };

  render() {
    const {currentUser, collapsed, fetchingNotices, getRouteData} = this.props;

    const renderAvatar = (size) => {
      let url = '';
      if (currentUser && currentUser.profile && currentUser.profile.photo && currentUser.profile.photo[size]) {
        url = currentUser.profile.photo[size];
      }
      return url.indexOf('http') > 0 ?
        <Avatar src={currentUser.profile.photo[size]}/>
        : <Avatar src={DEFAULT_AVATAR_URL}/>;
    };
    const renderUserNameDisplay = () => {
      if (currentUser && currentUser.profile) {
        return `${currentUser.profile.familyName} ${currentUser.profile.givenName}`;
      }
    };
    const renderRole = () => {
      if (currentUser && currentUser.profile) {
        const role = currentUser.profile.role.toLowerCase();
        switch (role) {
          case 'superadmin':
            return 'Super Administrator';
          case 'teacher':
            return 'Giáo viên';
          case 'student':
            return 'Sinh viên';
          case 'admin':
          default:
            return 'Administrator';
        }
      }
    };
    const menu = (
      <div>
        <div className={styles.profile_info}>
          <div className={styles.header_profile_avatar}>
            {renderAvatar('big')}
          </div>
          <div className={styles.detail}>
            <h1>{renderUserNameDisplay()}</h1>
            <span className={styles.roles}><span>{renderRole()}</span></span>
          </div>
        </div>
        <Menu className={styles.menu} selectedKeys={[]} onClick={this.onMenuClick}>
          <Menu.Item><Link to="/administrator/profile"><Icon type="user"/>Thông tin cá nhân</Link></Menu.Item>
          <Menu.Divider/>
          <Menu.Item key="logout"><a><Icon type="logout"/>Đăng xuất</a></Menu.Item>
        </Menu>
      </div>
    );
    const noticeData = this.getNoticeData();

    // Don't show popup menu when it is been collapsed
    const menuProps = collapsed ? {} : {
      openKeys: this.state.openKeys,
    };

    const layout = (
      <Layout>
        <Sider
          trigger={null}
          collapsible
          collapsed={collapsed}
          breakpoint="md"
          onCollapse={this.onCollapse}
          width={256}
          className={styles.sider}
        >
          <div className={styles.logo}>
            <Link to="/">
              <img src="/images/logo.png" alt="logo"/>
              <span style={{
                color: 'white',
                fontSize: '22px',
                fontWeight: '700',
              }}
              >ChoVaNhan
              </span>
            </Link>
          </div>
          <Menu
            theme="dark"
            mode="inline"
            {...menuProps}
            onOpenChange={this.handleOpenChange}
            selectedKeys={this.getCurrentMenuSelectedKeys()}
            style={{margin: '16px 0', width: '100%'}}
          >
            {this.getNavMenuItems(this.menus)}
          </Menu>
        </Sider>
        <Layout>
          <MainModal/>
          <Header className={styles.header}>
            <div className={styles.right}>
              <Popover
                content={menu}
                placement="bottom"
              >
                <a className={cx('ghost_link', 'flex_vcenter_link', styles.header_profile_avatar)}>
                  {renderAvatar('medium')}
                </a>
              </Popover>

              <NoticeIcon
                className={styles.action}
                count={currentUser ? currentUser.notifyCount : ''}
                onItemClick={(item, tabProps) => {
                  console.log(item, tabProps); // eslint-disable-line
                }}
                onClear={this.handleNoticeClear}
                onPopupVisibleChange={this.handleNoticeVisibleChange}
                loading={fetchingNotices}
                popupAlign={{offset: [20, -16]}}
              >
                <NoticeIcon.Tab
                  list={noticeData.notice}
                  title="Thông báo"
                  emptyText="Đang phát triển"
                  emptyImage="https://gw.alipayobjects.com/zos/rmsportal/wAhyIChODzsoKIOBHcBk.svg"
                />
                <NoticeIcon.Tab
                  list={noticeData.message}
                  title="Tin nhắn"
                  emptyText="Không có tin nhắn mới"
                  emptyImage="https://gw.alipayobjects.com/zos/rmsportal/sAuJeJzSKbUmHfBQRzmZ.svg"
                />
                <NoticeIcon.Tab
                  list={noticeData.tasks}
                  title="Thực thi"
                  emptyText="Không có nhiệm vụ mới"
                  emptyImage="https://gw.alipayobjects.com/zos/rmsportal/HsIsxMZiWKrNUavQUXqx.svg"
                />
              </NoticeIcon>
              {/* <div id="profile_dropdown_container"> */}
              {/* <Popover */}
              {/* content={menu} */}
              {/* placement="bottom" */}
              {/* getPopupContainer={() => document.getElementById('profile_dropdown_container')} */}
              {/* > */}
              {/* <Link to='/profile'><Icon type="ellipsis" /></Link> */}
              {/* </Popover> */}
              {/* </div> */}
            </div>
          </Header>
          <Content style={{height: '100%', padding: '20px'}}>
            <Switch>
              {
                getRouteData('BasicLayout').map(item =>
                  (
                    <Route
                      exact={item.exact}
                      key={item.path}
                      path={item.path}
                      handler={this.routeGuard}
                      component={item.component}
                    />
                  ),
                )
              }
              <Redirect exact from="/" to="/dashboard"/>
            </Switch>
            <GlobalFooter
              links={[{
                title: 'Trang chủ',
                href: '',
                blankTarget: true,
              }]}
              copyright={
                <div>
                  Copyright <Icon type="copyright"/> 2018 Smart Attendance
                </div>
              }
            />
          </Content>
        </Layout>
      </Layout>
    );

    return (
      <DocumentTitle title={this.getPageTitle()}>
        <ContainerQuery query={query}>
          {params => <div className={classNames(params)}>{layout}</div>}
        </ContainerQuery>
      </DocumentTitle>
    );
  }
}

export default connect(state => ({
  currentUser: state.passport.currentUser,
  currentHoverMenuItemKey: state.global.currentHoverMenuItemKey,
}))(BasicLayout);
