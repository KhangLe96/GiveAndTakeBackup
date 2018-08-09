import React from 'react';
import { connect } from 'dva';
import { routerRedux } from 'dva/router';
import { Button, Popconfirm, Row, Col, Divider } from 'antd';
import moment from 'moment';
import styles from './index.less';
import { DateFormatDisplay, ENG_VN_DICTIONARY, COLOR, STATUSES } from "../../../common/constants";

@connect(({ modals, postManagement }) => ({
  ...modals, postManagement,
}))

export default class index extends React.Component {
  componentDidMount() {
    const { dispatch } = this.props;
    const { id } = this.props.match.params;
    dispatch({
      type: 'postManagement/fetchPostInformation',
      payload: { id },
    });
  }

  handleRedirectToCategoryDetail = (category) => {
    const { dispatch } = this.props;
    dispatch({
      type: 'categoryManagement/getACategory',
      payload: {
        id: category.id,
      },
    });
    dispatch(routerRedux.push(`/category-management/detail/${category.id}`));
  }

  handleDateAndTimeFormat = (date) => {
    return (moment.utc(date).local().format(DateFormatDisplay));
  }

  handleRedirectToUserDetail = (user) => {
    const { dispatch } = this.props;
    dispatch({
      type: 'userManagement/getProfile',
      payload: {
        id: user.id,
      },
    });
    dispatch(routerRedux.push(`/user-management/detail/${user.id}`));
  }

  handleChangeCMSStatus = (e) => {
    const { dispatch } = this.props;
    const { id } = this.props.match.params;
    const { statusCMS } = this.props.postManagement.postInformation;
    const newStatusCMS = statusCMS === STATUSES.Blocked ? STATUSES.Activated : STATUSES.Blocked;
    dispatch({
      type: 'postManagement/changeAPostCMSStatus',
      payload: { newStatusCMS, id },
    });
  }

  handleActionWithPost = (record) => {
    let buttonContent = 'Khóa';
    let buttonIcon = 'lock';
    let buttonType = 'danger';
    let newPostStatus = STATUSES.Blocked;
    let popConfirmTitle = 'Bạn chắc chắn muốn khóa bài đăng này?';
    if (record.statusCMS === STATUSES.Blocked) {
      buttonContent = 'Mở khóa';
      buttonIcon = 'unlock';
      buttonType = 'default';
      newPostStatus = STATUSES.Activated;
      popConfirmTitle = 'Bạn có muốn mở lại bài đăng này?';
    }
    return (
      <span>
        <Popconfirm
          title={popConfirmTitle}
          onConfirm={() => this.handleChangeCMSStatus(record.key)}
        >
          <Button type={buttonType} icon={buttonIcon}>{buttonContent}</Button>
        </Popconfirm>
        <Divider type="vertical" />
        <Popconfirm
          title="Cảnh cáo người dùng này?"
        >
          <Button type="primary" icon="warning" style={{ background: COLOR.Warning }}>Cảnh báo người dùng</Button>
        </Popconfirm>
      </span >
    );
  }

  renderDetail(postInformation) {
    const { id, user, title, description, images, address, createdTime, updatedTime, statusCMS, statusApp, category } = postInformation !== null ? postInformation : null;
    return (
      <div>
        <div className="containerHeader">
          <h1>Chi tiết bài đăng</h1>
          <div className="rightButton">
            {this.handleActionWithPost(postInformation)}
          </div>
        </div>
        <div className="containerBody">
          <Row>
            <Col span={8}><h2> Tiêu đề </h2></Col>
            <Col span={8}><h2> Thông tin người dùng </h2></Col>
            <Col span={8}><h2> Địa chỉ </h2></Col>
          </Row>
          <Row>
            <Col span={8}><h3> {title} </h3></Col>
            <Col span={8}><h3> <a className={styles.detailText} onClick={() => this.handleRedirectToUserDetail(user)}> {user.username}</a></h3></Col>
            <Col span={8}><h3> {address.provinceCityName} </h3></Col>
          </Row>
          <Row>
            <Col span={8}><h2> Trạng thái </h2></Col>
            <Col span={8}><h2> Category </h2></Col>
            <Col span={8}><h2> Ngày tạo </h2></Col>
          </Row>
          <Row>
            <Col span={8}><h3 className={styles.statusText}> {ENG_VN_DICTIONARY[statusCMS]}</h3></Col>
            <Col span={8}><h3> <a className={styles.detailText} onClick={() => this.handleRedirectToCategoryDetail(category)}> {category.categoryName}</a></h3></Col>
            <Col span={8}><h3> {this.handleDateAndTimeFormat(createdTime)} </h3></Col>
          </Row>
          <Row>
            <Col><h2> Mô tả </h2></Col>
          </Row>
          <Row>
            <Col><h3> {description} </h3></Col>
          </Row>
          <br /><br /><br /><br />
          <Row>
            <Col span={8} >
              <img src='http://wfiles.brothersoft.com/c/cat-photograph_195928-800x600.jpg' alt="" className={styles.imageBox} />
              <div>
                {/* {(postInformation.imagePaths)
              ?
              postInformation.imagePaths.map((imagePath) => {
                return <img src={imagePath} key={imagePath} alt="" height="300" width="300" />;
              })
              : null} */}
              </div>
            </Col>
          </Row>
        </div>
      </div>
    );
  }

  render() {
    const { postManagement: { postInformation } } = this.props;
    const postDetail = (postInformation !== null) ? this.renderDetail(postInformation) : null;
    return (
      <div>
        {(postDetail) || null}
      </div>
    );
  }
}
