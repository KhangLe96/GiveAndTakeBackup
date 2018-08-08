import React from 'react';
import { connect } from 'dva';
import { Button, Popconfirm, Row, Col } from 'antd';
import { Record } from '../../../../node_modules/immutable';
import moment from 'moment';
import styles from './index.less';
import { DateFormatDisplay, STATUS_BLOCKED, STATUS_ACTIVATED, STATUS_ACTIVATED_VN, STATUS_BLOCKED_VN, ROLE_ADMIN_VN, ROLE_USER_VN } from '../../../common/constants';

@connect(({ modals, userManagement }) => ({
  ...modals, ...userManagement,
}))

export default class index extends React.Component {
  componentDidMount() {
    const { dispatch } = this.props;
    const { id } = this.props.match.params;
    dispatch({
      type: 'userManagement/getProfile',
      payload: { id },
    });
  }

  handleOnConfirm = (e) => {
    const { dispatch, users } = this.props;
    const { id } = this.props.match.params;
    const { status } = this.props.userProfile;
    const newStatus = status === STATUS_BLOCKED ? STATUS_ACTIVATED : STATUS_BLOCKED;
    dispatch({
      type: 'userManagement/changeStatusProfile',
      payload: { newStatus, id },
    });
  }

  handleDisplayStatusButton = (userProfile) => {
    userProfile.status === STATUS_ACTIVATED ? STATUS_BLOCKED_VN : STATUS_ACTIVATED_VN;
  }

  handleDisplayStatus = (status) => {
    status === STATUS_ACTIVATED ? STATUS_ACTIVATED_VN : STATUS_BLOCKED_VN;
  }

  handleDisplayRole = (role) => {
    role && role[0] === 'User' ? ROLE_USER_VN : ROLE_ADMIN_VN;
  }

  handleDateAndTimeFormat = (date) => {
    moment.utc(date).local().format(DateFormatDisplay)
  }

  renderDetail(userProfile) {
    const { username, status, firstName, lastName, birthdate, email, phoneNumber, gender, address, role } = userProfile !== null ? userProfile : null;
    return (
      <div>
        <div className="containerHeader">
          <h1>Xem thông tin thành viên</h1>
        </div>
        <div className="rightButton">
          <Popconfirm
            title="Bạn chắc không ?"
            onConfirm={() => this.handleOnConfirm(Record.key)}
          >
            <Button icon="exclamation-circle-o" type="primary" className={styles.buttonStyle}}>
                {this.handleDisplayStatusButton(userProfile)}
            </Button>
          </Popconfirm>
      </div>
      <div className="containerBody">
        <Row>
          <Col span={8} className={styles.imageBox}>
            <img src='https://cdn.pixabay.com/photo/2016/08/08/09/17/avatar-1577909_960_720.png' alt="" />
            <Row>
              <Col align="middle"><h2> {username} </h2></Col>
            </Row>
          </Col>
          <Col span={16}>
            <Row>
              <Col span={12}><h2> Tên người dùng </h2></Col>
              <Col span={12}><h2> Điện thoại </h2></Col>
            </Row>
            <Row>
              <Col span={12}><h3>{firstName} {lastName}</h3></Col>
              <Col span={12}><h3>{phoneNumber}</h3></Col>
            </Row>
            <br /><br />
            <Row>
              <Col span={12}><h2> Email </h2></Col>
              <Col span={12}><h2> Giới tính </h2></Col>
            </Row>
            <Row>
              <Col span={12}><h3>{email}</h3></Col>
              <Col span={12}><h3>{gender}</h3></Col>
            </Row>
            <br /><br />
            <Row>
              <Col span={12}><h2> Vai trò </h2></Col>
              <Col span={12}><h2> Trạng Thái </h2></Col>
            </Row>
            <Row>
              <Col span={12}><h3>{this.handleDateAndTimeFormat(birthdate)}</h3></Col>
              <Col span={12}><h3>{address}</h3></Col>
            </Row>
            <br /><br />
            <Row>
              <Col span={12}><h2> Vai trò </h2></Col>
              <Col span={12}><h2> Trạng Thái </h2></Col>
            </Row>
            <Row>
              <Col span={12}><h3>{this.handleDisplayRole(role)</h3></Col>
              <Col span={12}><h3>{this.handleDisplayStatus(status)}</h3></Col>
            </Row>
          </Col>
        </Row>
        <div>
          {/* {(users.avatarUrl)
                ?
                users.avatarUrl.map((avatarPath) => {
                  return <img src={avatarPath} key={avatarPath} alt="" height="400" width="400" />;
                })

                : null} */}
        </div>
        );
      }

  render() {
    const {userProfile} = this.props;
    const userDetail = (userProfile !== null) ? this.renderDetail(userProfile) : null;
    return (
      <div>
          {userDetail}
      </div>
    );
  }
}
