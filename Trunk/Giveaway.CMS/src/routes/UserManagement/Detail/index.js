import React from 'react';
import { connect } from 'dva';
import { Button, Popconfirm, Row, Col } from 'antd';
import { Record } from '../../../../node_modules/immutable';
import moment from 'moment';
import styles from './index.less';
import { DateFormatDisplay, STATUSES, STATUS_ACTION_ACTIVATE, STATUS_ACTION_BLOCK, ENG_VN_DICTIONARY } from '../../../common/constants';

@connect(({ modals, userManagement }) => ({
  ...modals, userManagement,
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
    const { dispatch } = this.props;
    const { id } = this.props.match.params;
    const { status } = this.props.userManagement.userProfile;
    const newStatus = status === STATUSES.Blocked ? STATUSES.Activated : STATUSES.Blocked;
    dispatch({
      type: 'userManagement/changeStatusProfile',
      payload: { newStatus, id },
    });
  }

  handleDisplayStatusButton = (userProfile) => {
    return (userProfile.status === STATUSES.Activated ? STATUS_ACTION_BLOCK : STATUS_ACTION_ACTIVATE);
  }

  handleDateAndTimeFormat = (date) => {
    return (moment.utc(date).local().format(DateFormatDisplay));
  }

  handleActionWithUser = (record) => {
    let buttonContent = 'Khóa';
    let buttonIcon = 'lock';
    let newStatus = STATUSES.Blocked;
    let popConfirmTitle = 'Bạn chắc chắn muốn khóa User này?';
    if (record.status === STATUSES.Blocked) {
      buttonContent = STATUS_ACTION_ACTIVATE;
      buttonIcon = 'unlock';
      newStatus = STATUSES.Activated;
      popConfirmTitle = 'Bạn có muốn mở lại User này?';
    }
    return (
      <span>
        <Popconfirm
          title={popConfirmTitle}
          onConfirm={() => this.handleOnConfirm(record.key)}
        >
          <Button type="primary" icon={buttonIcon} className={styles.buttonStyle}>{buttonContent}</Button>
        </Popconfirm>
      </span >
    );
  }

  displayImage = (avatarUrl) => {
    if ((avatarUrl === 'string') || (avatarUrl === null)) {
      return (<img src="./images/noImage.jpg" alt="" className={styles.avatarStyle} />)
    }
    else {
      return (<img src={avatarUrl} alt="" className={styles.avatarStyle} />)
    }
  }

  renderDetail(userProfile) {
    const { username, status, firstName, lastName, birthdate, email, phoneNumber, gender, address, role, avatarUrl } = userProfile !== null ? userProfile : null;
    return (
      <div>
        <div className="containerHeader">
          <h1>Xem thông tin thành viên</h1>
          <div className="rightButton">
            {this.handleActionWithUser(userProfile)}
          </div>
        </div>
        <div className="containerBody">
          <Row>
            <Col span={8} className={styles.imageBox}>
              {this.displayImage(avatarUrl)}
              <br /><br />
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
                <Col span={12}><h3>{ENG_VN_DICTIONARY[gender]}</h3></Col>
              </Row>
              <br /><br />
              <Row>
                <Col span={12}><h2> Ngày sinh </h2></Col>
                <Col span={12}><h2> Địa chỉ </h2></Col>
              </Row>
              <Row>
                <Col span={12}><h3>{this.handleDateAndTimeFormat(birthdate)}</h3></Col>
                <Col span={12}><h3>{address}</h3></Col>
              </Row>
              <br /><br />
              <Row>
                <Col span={12}><h2> Vai trò </h2></Col>
                <Col span={12}><h2> Trạng thái </h2></Col>
              </Row>
              <Row>
                <Col span={12}><h3>{ENG_VN_DICTIONARY[role && role[0]]} {ENG_VN_DICTIONARY[role && role[1]]}</h3></Col>
                <Col span={12}><h3 className={styles.statusText}>{ENG_VN_DICTIONARY[status]}</h3></Col>
              </Row>
            </Col>
          </Row>
        </div>
      </div>

    );
  }
  render() {
    const { userManagement: { userProfile } } = this.props;
    const userDetail = (userProfile !== null) ? this.renderDetail(userProfile) : null;
    return (
      <div>
        {userDetail}
      </div>
    );
  }
}
