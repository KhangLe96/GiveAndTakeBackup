import React from 'react';
import styles from './Dashboard.less';
import { TimelineChart } from 'ant-design-pro/lib/Charts';
import { Bar } from 'ant-design-pro/lib/Charts/index';
import { Button, Spin } from 'antd';
import AccountHelper from '../../helpers/AccountHelper';
import { connect } from 'dva';

const chartData = [];
for (let i = 0; i < 20; i += 1) {
  chartData.push({
    x: (new Date().getTime()) + (1000 * 60 * 30 * i),
    y1: Math.floor(Math.random() * 1000) + 1000,
    y2: Math.floor(Math.random() * 9000) + 10,
  });
}

const salesData = [];
for (let i = 0; i < 12; i += 1) {
  salesData.push({
    x: `${i + 1}VN  `,
    y: Math.floor(Math.random() * 1000) + 200,
  });
}

@connect(state => ({
  dashboard: state.dashboard,
}))
class Dashboard extends React.Component {
  state = {};

  componentDidMount(props) {

  }

  detection = () => {
    this.props.dispatch({
      type: 'dashboard/detection',
    });
  }

  isAdmin = () => {
    const isAdmin = AccountHelper.getCurrentRole() === 'Admin';
    return isAdmin;
  }

  render() {
    const { dashboard: { loading } } = this.props;
    return (<div className={styles.dashboard}>
      <Spin spinning={loading}>
        <div style={{ textAlign: 'center' }}>
          {this.isAdmin() ? <Button size="large" type="primary" onClick={this.detection}>Detection</Button> : null
            }
        </div>
      </Spin>
      <Bar
        height={200}
        title="Attendance"
        data={salesData}
      />
      <TimelineChart
        height={400}
        data={chartData}
        titleMap={{ y1: 'View', y2: 'Access' }}
      />
    </div>
    );
  }
}

export default Dashboard;
