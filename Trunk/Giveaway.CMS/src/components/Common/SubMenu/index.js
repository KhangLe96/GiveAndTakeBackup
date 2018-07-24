import { Tabs, Radio, Row, Col, Layout } from 'antd';
import { routerRedux } from 'dva/router';
import styles from './SubMenu.less';
import { connect } from 'dva';
import MyTripPage from '../../../routes/Profile/MyTripPage';
import ProfileInfoPage from '../../../routes/Profile/ProfileInfoPage';
import style from '../../../layouts/MainLayout.less';

const { TabPane } = Tabs;
const { PureComponent } = React;
const { Content } = Layout;

@connect(state => ({
  ...state,
}))
export default class SubMenu extends PureComponent {
  onTabClick = (activeKey) => {
    const url = ['/#/profile/info', '/#/profile/mytrip', '/#/profile/info', '/#/profile/info', '/#/profile/info', '/#/profile/info'];
    window.location.href = url[activeKey - 1];
  }

  render() {
    const { location, activeKey } = this.props;
    return (
      <Content>
        <div className={styles.sub_menu}>
          <div className={styles.fake_tab} />
          <div className={styles.tab}>
            <Row type="flex" justify="center">
              <Col className={`fixed_width_content ${style.header_container_inner}`}>
                <Tabs
                  defaultActiveKey={activeKey}
                  onTabClick={this.onTabClick}
                  size="large"
                >
                  <TabPane tab="Thông tin chung" key="1" />
                  <TabPane tab="Chuyến xe của tôi" key="2" />
                  <TabPane tab="Lịch sử chuyến xe" key="3" />
                  <TabPane tab="Lịch sử thanh toán" key="4" />
                  <TabPane tab="Ví điện tử" key="5" />
                  <TabPane tab="Giấy tờ" key="6" />
                </Tabs>
              </Col>
            </Row>
          </div>
        </div>
      </Content>
    );
  }
}
